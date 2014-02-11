package com.gremkil.trying;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.Field;
import java.net.URL;
import java.util.Enumeration;
import java.util.List;

import dalvik.system.DexFile;
import dalvik.system.PathClassLoader;

/**
 * Created by User on 11.02.14.
 */
public class ReflectionHelper {

    public static List<Class<?>> getClasses() {
        Field[] fileds = PathClassLoader.class.getFields();
        fileds.clone();
        return null;
    }
}
