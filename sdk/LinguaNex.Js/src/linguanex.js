const localesData = {};
const axios = require('axios');
var linguaNexOptions = {};
// 从API获取国际化资源数据
function loadApiResources(prject, locale) {
    return axios.get(`${linguaNexOptions.baseUrl}/api/OpenApi/Resources/${prject}`, {
        headers: {
            "Accept-Language": locale
        }
    })
    .then(res => {
        localesData[locale] = res.data[0].resources
        return locale
    })
    .catch(err => {
        return new Error(err.message)
    })
    ;
  }
function setLocale(locale){
    if(!localesData[locale]){
        linguaNexOptions.currentLocale = locale;
        return loadApiResources(linguaNexOptions.project, locale, () => {});
    }
    else{
        linguaNexOptions.currentLocale = locale;
        return new Promise(s => s(null));
    }
}
// 获取国际化文本
function L(key, locale, defaultStr) {
    if(!locale)
        locale = linguaNexOptions.currentLocale
    const data = localesData[locale];
    if(data[key])
        return data[key];
    return defaultStr || key;
  }

function initLinguaNex(options) {
    linguaNexOptions = options;
    linguaNexOptions.currentLocale = options.defaultLocale;
    const promises = [];
    for(let locale in options.locales){
        promises.push(loadApiResources(options.project, options.locales[locale]));
    }
    return Promise.all(promises)
    .then(results => {
        console.log('Init Finish', results)
    })
    .catch(err => {
       console.error('Error:', err) 
    })
}
module.exports = {
    initLinguaNex: initLinguaNex,
    L: L,
    setLocale: setLocale
  };