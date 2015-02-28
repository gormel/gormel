package com.gremkil.trying.utils;

public class MathUtils {
    private MathUtils() {}

    public static float length(float x, float y) {
        return (float)Math.sqrt(x * x + y * y);
    }

    public static float length(float x, float y, float z) {
        return (float)Math.sqrt(x * x + y * y + z * z);
    }

    public static void norm(float[] v) {
        float sum = 0;
        for (float x : v) {
            sum += x * x;
        }
        float length = (float)Math.sqrt(sum);
        for (int i = 0; i < v.length; i++) {
            v[i] /= length;
        }
    }

    public static void rotateVectorByQuaternion(float[] v, float qx, float qy, float qz, float qw) {
        float vx = v[0];
        float vy = v[1];
        float vz = v[2];

        qx = -qx; qy = -qy; qz = -qz;

        float a = +qy * vx - qx * vy + qw * vz;
        float b = -qz * vx + qw * vy + qx * vz;
        float c = +qw * vx + qz * vy - qy * vz;
        float d = +qx * vx + qy * vy + qz * vz;

        v[0] = -qy * a + qz * b + qw * c + qx * d;
        v[1] = +qx * a + qw * b - qz * c + qy * d;
        v[2] = +qw * a - qx * b + qy * c + qz * d;
    }
}
