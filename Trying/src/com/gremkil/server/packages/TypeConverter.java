package com.gremkil.server.packages;

import java.nio.ByteBuffer;
import java.nio.charset.Charset;

public class TypeConverter {
    private static TypeConverter inst = null;
    public static TypeConverter getInstance() {
        if (inst == null)
            inst = new TypeConverter();
        return inst;
    }

    public Package processPackage(byte[] data) {
        ByteBuffer buffer = ByteBuffer.wrap(data);
        int packageId = buffer.getInt();
        Class<? extends Package> packageClass = PackageStorage.getInstance().getPackageClass(packageId);
        try {
            packageClass.getConstructor();
        } catch (NoSuchMethodException e) {
            throw new RuntimeException(e);
        }

        throw new UnsupportedOperationException();
    }

    public Object getObject(byte[] data) {
        throw new UnsupportedOperationException();
    }

    public byte[] getData(Object obj) {
        if (obj.getClass().equals(byte.class) || obj.getClass().equals(Byte.class))
            return new byte[] { ((Byte)obj).byteValue() };

        if (obj.getClass().equals(short.class) || obj.getClass().equals(Short.class)) {
            ByteBuffer byteBuffer = ByteBuffer.allocate(2);
            byteBuffer.putShort((Short) obj);
            return byteBuffer.array();
        }

        if (obj.getClass().equals(int.class) || obj.getClass().equals(Integer.class)) {
            ByteBuffer byteBuffer = ByteBuffer.allocate(Integer.SIZE / 8);
            byteBuffer.putInt((Integer) obj);
            return byteBuffer.array();
        }

        if (obj.getClass().equals(long.class) || obj.getClass().equals(Long.class)) {
            ByteBuffer byteBuffer = ByteBuffer.allocate(Long.SIZE / 8);
            byteBuffer.putLong((Long) obj);
            return byteBuffer.array();
        }

        if (obj.getClass().equals(float.class) || obj.getClass().equals(Float.class)) {
            ByteBuffer byteBuffer = ByteBuffer.allocate(Float.SIZE / 8);
            byteBuffer.putFloat((Float) obj);
            return byteBuffer.array();
        }

        if (obj.getClass().equals(double.class) || obj.getClass().equals(Double.class)) {
            ByteBuffer byteBuffer = ByteBuffer.allocate(Double.SIZE / 8);
            byteBuffer.putDouble((Double) obj);
            return byteBuffer.array();
        }

        if (obj.getClass().equals(boolean.class) || obj.getClass().equals(Boolean.class)) {
            return new byte[] { (byte)((Boolean)obj ? 0 : 1) };
        }
          
        if (obj.getClass().equals(char.class) || obj.getClass().equals(Character.class)) {
            ByteBuffer byteBuffer = ByteBuffer.allocate(Character.SIZE / 8);
            byteBuffer.putChar((Character) obj);
            return byteBuffer.array();
        }

        if (obj.getClass().equals(String.class)) {
            return ((String)obj).getBytes(Charset.forName("UTF-8"));
        }
        throw new IllegalArgumentException();
    }
}
