package com.gremkil.server.packages;

import org.apache.http.util.ByteArrayBuffer;

import java.nio.ByteBuffer;
import java.nio.MappedByteBuffer;

public class PackageConverter {
    private static PackageConverter inst = null;
    public static PackageConverter getInstance() {
        if (inst == null)
            inst = new PackageConverter();
        return inst;
    }

    public Package processPackage(byte[] data) {
        ByteBuffer buffer = ByteBuffer.wrap(data);
        int packageId = buffer.getInt();

        return null;
    }
}
