const localesData = {};
const axios = require('axios');
var linguaNexOptions = {};
// 从API获取国际化资源数据
function loadApiResources(prject, locale, callback) {
    axios.get(`${linguaNexOptions.baseUrl}/api/OpenApi/Resources/${prject}`, {
        headers: {
            "Accept-Language": locale
        }
    })
    .then(res => {
        localesData[locale] = res.data[0].resources
        callback()
    })
    .catch(err => {
        console.log(err)
    })
    ;
  }
function setLocale(locale){
    if(!localesData[locale])
        loadApiResources(linguaNexOptions.project, locale, () => {});
    else
        linguaNexOptions.currentLocale = locale;
}
// 获取国际化文本
function L(key, locale, defaultStr) {
    if(!locale)
        locale = linguaNexOptions.currentLocale
    const data = localesData[locale];
    return data[key] || defaultStr || key;
  }

function initLinguaNex(options, callback) {
    linguaNexOptions = options;
    linguaNexOptions.currentLocale = options.defaultLocale;
    for(let locale in options.locales){
        loadApiResources(options.project, options.locales[locale], () => {
            callback(options.locales[locale])
        });
    }
}
module.exports = {
    initLinguaNex: initLinguaNex,
    L: L,
    setLocale: setLocale
  };