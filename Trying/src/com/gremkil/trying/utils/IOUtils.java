package com.gremkil.trying.utils;

import java.io.IOException;
import java.io.InputStream;

public class IOUtils {
    public static void close(InputStream is) {
        if (is == null) { return; }
        try {
            is.close();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
}
