package com.linguanex.national.util;

import java.io.IOException;
import java.io.OutputStream;
import java.util.Properties;

public class PropUtils {

    public static void writeProp(Properties prop, OutputStream os) {
        try {
            prop.store(os, "Comment");
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (os != null) {
                try {
                    os.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
