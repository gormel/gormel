using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packages;

namespace RoomsServer
{
	public abstract class Session<T> where T : ClientInfo
	{
		public ObservableCollection<T> Clients { get; private set; }
		public Session()
		{
			Clients = new ObservableCollection<T>();
			Clients.CollectionChanged += Clients_CollectionChanged;
		}

		void Clients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach(var item in e.NewItems)
					{
						T info = (T)item;
						info.Client.PackageRecive += Client_PackageRecive;
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var item in e.OldItems)
					{
						T info = (T)item;
						info.Client.PackageRecive -= Client_PackageRecive;
					}
					break;
				case NotifyCollectionChangedAction.Reset:
					foreach (var client in Clients)
					{
						T info = (T)client;
						info.Client.PackageRecive -= Client_PackageRecive;
					}
					break;
				default:
					break;
			}
		}

		void Client_PackageRecive(object sender, Package e)
		{
			Client c = (Client)sender;
			T info = Clients.Where(i => i.Client == c).First();
			OnPackageRecive(info, e);
		}

		protected abstract void OnPackageRecive(T info, Package pack);
	}
}
