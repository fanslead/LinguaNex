package com.linguanex.national.config;

import org.yaml.snakeyaml.Yaml;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.InputStream;
import java.util.HashMap;
import java.util.Map;

public class GlobalProp {


    public static String REMOTE_URL;

    public static String KEY_WORD;

    public static void initFromYaml(String path) {
        if (path == null) {
            path = "national.yml";
        }
        Yaml yaml = new Yaml();
        Map<String, String> configMap = new HashMap<>(8);
        try {
            InputStream is = new FileInputStream(path);
            configMap = yaml.load(is);
        } catch (FileNotFoundException e) {
            throw new RuntimeException(e);
        }
        REMOTE_URL = configMap.get("national.remote.url");
        KEY_WORD = configMap.get("national.remote.key");
    }

}
