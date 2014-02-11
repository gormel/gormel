package com.gremkil.server.packages;

public abstract class Package {
    protected Package() {

    }

    public int getIndex() {
        return getClass().getAnnotation(IsPackage.class).id();
    }

    public byte[] getBytes() {
        return null;
    }


}
