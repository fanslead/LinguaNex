const linguanex = require('./src/linguanex');
linguanex.initLinguaNex({
    baseUrl: 'http://47.119.20.111',
    locales: ["zh-CN", "en"],
    defaultLocale: 'zh-CN',
    project: 'C96755D0-C22C-4DAD-9620-AF64C4C3D9D7'
})
module.exports = {
    linguanex: linguanex
};