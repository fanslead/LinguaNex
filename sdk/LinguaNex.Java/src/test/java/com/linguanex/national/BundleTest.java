package com.linguanex.national;

import com.linguanex.national.component.RemoteSourceBundle;
import com.linguanex.national.config.GlobalProp;
import org.springframework.context.annotation.Bean;
import org.springframework.context.support.ResourceBundleMessageSource;

import java.util.Locale;

public class BundleTest {
    @Bean
    ResourceBundleMessageSource messageSource() {
        return new RemoteSourceBundle();
    }

    public static void main(String[] args) {
        ResourceBundleMessageSource source = new RemoteSourceBundle();
        GlobalProp.initFromYaml(null);
        Locale locale = new Locale("zh-Hans");
        BundleTest test = new BundleTest();
        System.out.println(source.getMessage("40004", null, locale));
    }
}
