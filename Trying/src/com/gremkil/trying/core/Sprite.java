package com.gremkil.trying.core;

import android.graphics.Bitmap;

import java.util.ArrayList;
import java.util.List;

public class Sprite {
    private List<Bitmap> pictures = new ArrayList<Bitmap>();

    public static Sprite load(Bitmap source, int hCount, int vCount) {
        List<Bitmap> images = new ArrayList<Bitmap>();
        int height = source.getHeight() / vCount;
        int width = source.getWidth() / hCount;
        for (int vIndex = 0; vIndex < vCount; vIndex++) {
            for (int hIndex = 0; hIndex < hCount; hIndex++) {
                int x = hIndex * width;
                int y = vIndex * height;
                Bitmap subImage = Bitmap.createBitmap(source, x, y, width, height);
                images.add(subImage);
            }
        }
        return new Sprite(images);
    }

    public Sprite(Bitmap image) {
        pictures.add(image);
    }

    public Sprite(List<Bitmap> images) {
        pictures.addAll(images);
    }

    public Bitmap getImage() {
        return pictures.get(0);
    }

    public Bitmap getImage(int num) {
        int size = pictures.size();
        if (num < 0) { num = size - Math.abs(num % size) - 1; }
        else { num = num % size; }
        return pictures.get(num);
    }

    public int getImageCount() {
        return pictures.size();
    }

    public int getWidth() { return getImage().getWidth(); }

    public int getHeight() { return getImage().getHeight(); }
}
