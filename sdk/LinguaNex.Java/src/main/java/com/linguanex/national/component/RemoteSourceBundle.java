package com.linguanex.national.component;

import cn.hutool.core.collection.CollUtil;
import com.linguanex.national.service.RemoteConfigService;
import org.springframework.context.support.ResourceBundleMessageSource;

import java.io.IOException;
import java.io.InputStream;
import java.util.Locale;
import java.util.PropertyResourceBundle;
import java.util.ResourceBundle;
import java.util.Set;

public class RemoteSourceBundle extends ResourceBundleMessageSource {

    @Override
    public Set<String> getBasenameSet() {
        return CollUtil.set(false, "");
    }

    @Override
    public ResourceBundle doGetBundle(String basename, Locale locale){
        ResourceBundle bundle = null;
        InputStream stream = null;
        try {
            RemoteConfigService service = RemoteConfigService.getInstance();
            stream = service.buildFormatStream(locale);
            bundle = new PropertyResourceBundle(stream);
        } catch (IOException ioException) {
            ioException.printStackTrace();
        } finally {
            try {
                if (stream != null) {
                    stream.close();
                }
            } catch (IOException ignored) {  }
        }
        return bundle;
    }
}
