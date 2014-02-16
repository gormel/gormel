package com.gremkil.server.packages;

import java.lang.annotation.Annotation;
import java.lang.reflect.Array;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

public abstract class Package {
    protected Package() {

    }

    public int getIndex() {
        return getClass().getAnnotation(IsPackage.class).id();
    }

    public Byte[] getBytes() {
        List<Field> storage = new ArrayList<Field>();
        for (Field f : getClass().getFields())
            if (f.getAnnotation(IsData.class) != null)
                storage.add(f);

        Collections.sort(storage, new Comparator<Field>() {
            @Override
            public int compare(Field a, Field b) {
                IsData aIndex = a.getAnnotation(IsData.class);
                IsData bIndex = b.getAnnotation(IsData.class);
                return aIndex.index() - bIndex.index();
            }
        });

        List<Byte> result = new ArrayList<Byte>();
        for (Field f : storage) {
            try {
                for (byte b : TypeConverter.getInstance().getData(f.get(this))) {
                    result.add(b);
                }
            } catch (IllegalAccessException e) {
                e.printStackTrace();
            }
        }

        return result.toArray(new Byte[0]);
    }


}
