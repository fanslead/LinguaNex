package com.linguanex.national.vo;

import java.util.Map;

public class LangPackageItemVO {

    private String cultureName;

    private Map<String, String> resources;

    public String getCultureName() {
        return cultureName;
    }

    public void setCultureName(String cultureName) {
        this.cultureName = cultureName;
    }

    public Map<String, String> getResources() {
        return resources;
    }

    public void setResources(Map<String, String> resources) {
        this.resources = resources;
    }
}
