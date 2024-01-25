package linguanex

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
)

// LinguaNexOptions 定义了初始化LinguaNex所需的选项
type LinguaNexOptions struct {
	BaseUrl       string
	Project       string
	DefaultLocale string
	Locales       map[string]string
}

// localesData 存储所有本地化资源数据
var localesData = make(map[string]map[string]interface{})
var linguaNexOptions *LinguaNexOptions

// loadApiResources 从API获取国际化资源数据
func loadApiResources(project, locale string) (*http.Response, error) {
	req, err := http.NewRequest("GET", fmt.Sprintf("%s/api/OpenApi/Resources/%s", linguaNexOptions.BaseUrl, project), nil)
	if err != nil {
		return nil, err
	}
	req.Header.Set("Accept-Language", locale)

	client := &http.Client{}
	resp, err := client.Do(req)
	if err != nil {
		return nil, err
	}

	return resp, nil
}

// processApiResponse 处理API响应并填充localesData
func processApiResponse(locale string, resp *http.Response) error {
	defer resp.Body.Close()
	bodyBytes, err := ioutil.ReadAll(resp.Body)
	if err != nil {
		return err
	}

	var data []struct {
		Resources map[string]interface{} `json:"resources"`
	}
	err = json.Unmarshal(bodyBytes, &data)
	if err != nil {
		return err
	}

	if len(data) > 0 && data[0].Resources != nil {
		localesData[locale] = data[0].Resources
	} else {
		localesData[locale] = make(map[string]interface{})
	}

	return nil
}

// SetLocale 设置当前区域设置并加载资源
func SetLocale(locale string) error {
	if _, exists := localesData[locale]; !exists {
		linguaNexOptions.CurrentLocale = locale
		resp, err := loadApiResources(linguaNexOptions.Project, locale)
		if err != nil {
			return err
		}
		return processApiResponse(locale, resp)
	} else {
		linguaNexOptions.CurrentLocale = locale
		return nil
	}
}

// GetLocale 获取指定区域设置的资源
func GetLocale(locale string) map[string]interface{} {
	data, exists := localesData[locale]
	if exists {
		return data
	}
	return make(map[string]interface{})
}

// GetAllLocale 获取所有区域设置的资源
func GetAllLocale() map[string]map[string]interface{} {
	return localesData
}

// L 获取国际化文本
func L(key string, locale ...string) string {
	defaultStr := ""
	if len(locale) == 0 || locale[0] == "" {
		locale = []string{linguaNexOptions.CurrentLocale}
	}
	data := localesData[locale[0]]
	if data != nil && data[key] != nil {
		return data[key].(string)
	}
	return defaultStr
}

// Init 初始化LinguaNex实例
func Init(options LinguaNexOptions) error {
	linguaNexOptions = &options

	var promises []*http.Response
	for loc := range options.Locales {
		resp, err := loadApiResources(options.Project, loc)
		if err != nil {
			fmt.Println("Error:", err)
			continue
		}
		promises = append(promises, resp)
	}

	for _, resp := range promises {
		err := processApiResponse("", resp)
		if err != nil {
			fmt.Println("Error processing API response:", err)
		}
	}

	fmt.Println("Init Finish")
	return nil
}
