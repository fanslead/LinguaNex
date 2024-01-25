package main

import "./linguanex"

func main() {
	options := linguanex.LinguaNexOptions{
		BaseUrl:       "http://47.119.20.111",
		Project:       "C96755D0-C22C-4DAD-9620-AF64C4C3D9D7",
		DefaultLocale: "en",
		Locales: map[string]string{
			"en":    "English",
			"zh-CN": "Chinese",
		},
	}

	err := linguanex.Init(options)
	if err != nil {
		// Handle the error
	}

	_ = linguanex.SetLocale("en")
	text := linguanex.L("some_key")

	// ...
}
