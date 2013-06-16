using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CopyingModel
{
	public class LandView : DrawObject<Land, Land.States>
	{
		public float Width { get; private set; }
		public float Height { get; private set; }

		private float enteryWidth;
		private float enteryHeight;

		private Dictionary<LandEntery, Animation> animations = new Dictionary<LandEntery, Animation>();
		private List<Animation> otherAnimations = new List<Animation>();
		private List<Animation> removing = new List<Animation>();

		public LandView(Land land, SpriteBatch spriteBatch, float width, float height)
			: base(land, spriteBatch)
		{
			Width = width;
			Height = height;

			enteryWidth = Width / land.Width;
			enteryHeight = Height / land.Height;

			land.StateChanged += land_StateChanged;
		}

		void land_StateChanged(object sender, StateChangedEventArgs<Land.States> e)
		{
			switch (e.State)
			{
				case Land.States.MonsterCreated:
					//TODO: анимация появления монстра
					var mon = (Monster)e.Args[0];
					var sprites = ContentManager.MonsterStandAnimation;

					Animation anim = CreateSpriteAnimation(mon, sprites);
					anim.AnimationEnd += (s, ev) =>
						{
							((Animation)s).Start();
						};	
					anim.Start();

					animations.Add(mon, anim);
					break;
				case Land.States.TowerCreated:
					var tow = (Tower)e.Args[0];
					var tPos = (Point)e.Args[1];
					//TODO башенки, нарисовать анимацию и добавить в список
					DrawSettings settings1 = new DrawSettings();
					settings1.Texture = ContentManager.TowerStandAnimation;
					settings1.Position = new Vector2(tPos.X * enteryWidth + enteryWidth / 2, tPos.Y * enteryHeight + enteryHeight / 2);
					settings1.Scale = new Vector2(enteryWidth / settings1.Texture.Width, 
												enteryHeight / settings1.Texture.Height);
					settings1.Origin = new Vector2(settings1.Texture.Width, settings1.Texture.Height) / 2;
					Animation towerAnimation = new StaticAnimation(spriteBatch, settings1);

					animations.Add(tow, towerAnimation);
					break;
				case Land.States.TowerShoot:
					var tower = (Tower)e.Args[0];
					var towPos = (Point)e.Args[1];
					
					CreateShootAnimation(tower);
					break;
				case Land.States.TowerDied:
				case Land.States.MonsterDied:
					var monster = (LandEntery)e.Args[0];
					var mWhere = (Point)e.Args[1];
					//TODO: анимация смерти
					animations.Remove(monster);
					break;
				default:
					break;
			}
		}

		public override void Update(GameTime time)
		{
			foreach (var anim in removing)
			{
				otherAnimations.Remove(anim);
			}
			removing.Clear();

			foreach (var anim in animations.Values)
			{
				anim.Update(time);
			}

			foreach (var anim in otherAnimations)
			{
				anim.Update(time);
			}
		}

		public override void Draw(GameTime time)
		{
			foreach (var anim in animations.Values)
			{
				anim.Draw(time);
			}

			foreach (var anim in otherAnimations)
			{
				anim.Draw(time);
			}
		}

		private void CreateShootAnimation(Tower tower)
		{
			var towPos = tower.Position;
			Vector2 scaleFrom = new Vector2(enteryWidth / ContentManager.ShootTexture.Width,
											enteryHeight / ContentManager.ShootTexture.Height);
			Vector2 scaleTo = scaleFrom * tower.Radius * 2;

			DrawSettings settings2 = new DrawSettings();
			settings2.Texture = ContentManager.ShootTexture;
			settings2.Position = new Vector2(towPos.X * enteryWidth + enteryWidth / 2, towPos.Y * enteryHeight + enteryHeight / 2);
			settings2.Origin = new Vector2(settings2.Texture.Width, settings2.Texture.Height) / 2;
			Animation shootAnim =
				new ScaleAnimation(spriteBatch, settings2, scaleFrom, scaleTo,
					TimeSpan.FromMilliseconds(Tower.shootTimeout.TotalMilliseconds / 4));
			shootAnim.Start();
			shootAnim.AnimationEnd += (s, ev) => removing.Add((Animation)s);

			otherAnimations.Add(shootAnim);
		}

		private Animation CreateSpriteAnimation(LandEntery entery, IEnumerable<Texture2D> sprites)
		{
			DrawSettings settings = new DrawSettings();
			settings.Scale = new Vector2(enteryWidth / sprites.First().Width,
										enteryHeight / sprites.First().Height);
			settings.Origin = new Vector2(sprites.First().Width, sprites.First().Height) / 2;
			settings.Position = new Vector2(entery.Position.X * enteryWidth + enteryWidth / 2, 
											entery.Position.Y * enteryHeight + enteryHeight / 2);

			SpriteAnimation anim = new SpriteAnimation(spriteBatch, settings, sprites, TimeSpan.FromMilliseconds(250 * 6));
			return anim;
		}
	}
}
