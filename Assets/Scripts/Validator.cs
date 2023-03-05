using System;

public static class Validator {
    public static void CheckNotNull(object obj, string exceptionMessage) {
        if (obj == null) {
            throw new Exception(exceptionMessage);
        }
    }
}