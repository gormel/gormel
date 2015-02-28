package com.gremkil.trying.core;

import android.graphics.Bitmap;

public class Animation {
    private Sprite sprite;
    private final float frameDurationMs;
    private final int startFrame;
    private final int endFrame;
    private float accumulatedTimeMs = 0;
    private int frameIndex = 0;

    public Animation(Sprite sprite, float spriteFrameDurationMs, int startFrame, int endFrame) {
        this.sprite = sprite;
        this.frameDurationMs = spriteFrameDurationMs;
        this.startFrame = startFrame;
        this.endFrame = endFrame;
    }

    public void update(float elapsedMs) {
        accumulatedTimeMs += elapsedMs;
        while (accumulatedTimeMs > frameDurationMs) {
            accumulatedTimeMs -= frameDurationMs;
            frameIndex = (frameIndex + 1) % (endFrame - startFrame);
        }
    }

    public Bitmap getFrame() {
        return sprite.getImage(startFrame + frameIndex);
    }
}
