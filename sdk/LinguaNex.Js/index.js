const { initLinguaNex, setLocale, getLocale, getAllLocale, L } = require('./src/linguanex');
initLinguaNex({
    baseUrl: 'http://47.119.20.111',
    locales: ["zh-CN", "en"],
    defaultLocale: 'zh-CN',
    project: 'C96755D0-C22C-4DAD-9620-AF64C4C3D9D7'
})
.then(() => {
    console.log(L('Hello'));
    setLocale('aa')
    .then(() => {
        console.log(L('Hello'));
        console.log(getAllLocale());
    })
    console.log(getLocale("zh-CN"));
})
module.exports = {
    linguanex: linguanex
};