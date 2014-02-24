package com.gremkil.server.packages;

import java.lang.annotation.Annotation;
import java.util.ArrayList;
import java.util.List;

import dalvik.system.PathClassLoader;

/**
 * Created by Tyulen on 16.02.14.
 */
public class PackageStorage {
    private static PackageStorage inst = null;
    public static PackageStorage getInstance() {
        if (inst == null)
            inst = new PackageStorage();
        return inst;
    }

    private List<Class<?>> registredClasses = new ArrayList<Class<?>>();

    public void registerClass(Class<?> _class) {
        if (registredClasses.contains(_class))
            return;
        registredClasses.add(_class);
    }

    public List<Class<? extends Package>> getPackageClasses() {
        List<Class<? extends  Package>> result = new ArrayList<Class<? extends Package>>();
        for (Class c : registredClasses) {
            if (c.isAssignableFrom(Package.class)) {
                Annotation a = c.getAnnotation(IsPackage.class);
                if (a != null) {
                    result.add(c);
                }
            }
        }
        return result;
    }

    public Class<? extends Package> getPackageClass(int id) {
        List<Class<? extends Package>> packageClasses = getPackageClasses();
        for (Class<? extends Package> packageClass : packageClasses) {
            IsPackage isPackageAnnotation = packageClass.getAnnotation(IsPackage.class);
            if (isPackageAnnotation != null) {
                if (isPackageAnnotation.id() == id) {
                    return packageClass;
                }
            }
        }
        return null;
    }
}
