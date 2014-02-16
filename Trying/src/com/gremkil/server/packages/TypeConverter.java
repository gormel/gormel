package com.gremkil.server.packages;

import java.nio.ByteBuffer;

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

        throw new UnsupportedOperationException();
    }

    public byte[] getData(Object obj) {
        throw new UnsupportedOperationException();
    }
}
