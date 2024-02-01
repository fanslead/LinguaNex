package com.linguanex.national.service;

import cn.hutool.http.HttpRequest;
import cn.hutool.http.HttpResponse;
import cn.hutool.json.JSONUtil;
import com.linguanex.national.config.GlobalProp;
import com.linguanex.national.util.PropUtils;
import com.linguanex.national.vo.LangPackageItemVO;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.util.List;
import java.util.Locale;
import java.util.Optional;
import java.util.Properties;

public class RemoteConfigService {

    private RemoteConfigService() {
    }

    private static volatile RemoteConfigService remoteConfigService;

    public static RemoteConfigService getInstance() {
        if (remoteConfigService == null) {
            synchronized (RemoteConfigService.class) {
                return Optional.ofNullable(remoteConfigService).orElse(new RemoteConfigService());
            }
        }
        return remoteConfigService;
    }

    /**
     * 请求和构造符合数据格式的流
     */
    public InputStream buildFormatStream(Locale locale) {
        ByteArrayOutputStream bos = new ByteArrayOutputStream();

        HttpResponse response = HttpRequest.get(GlobalProp.REMOTE_URL + "/" + GlobalProp.KEY_WORD + "?cultureName="
                + languageConvent(locale.getLanguage())).execute();

        if (!response.isOk()) {
            throw new RuntimeException("fail on remote call: " + response.getStatus());
        }
        List<LangPackageItemVO> dataList = JSONUtil.toList(response.body(), LangPackageItemVO.class);
        if (dataList.isEmpty()) {
            throw new RuntimeException("empty data");
        }
        LangPackageItemVO itemVO = dataList.iterator().next();

        Properties dataProp = new Properties();
        itemVO.getResources().forEach(dataProp::setProperty);
        PropUtils.writeProp(dataProp, bos);
        ByteArrayInputStream swapStream = new ByteArrayInputStream(bos.toByteArray());
        return swapStream;
    }

    private static String languageConvent(String lan){
        switch (lan) {
            case "zh-hans":
                return "zh-Hans";
            default:
                return lan;
        }
    }

}
