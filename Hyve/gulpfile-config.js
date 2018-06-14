// Gulp configuration

module.exports = {
    srcPath: "_src",
    destPath: "assets",
    concattedCssFilename: "main.css",
    concattedJsFilename: "main.js",
    concattedMinifiedCssFilename: "main.min.css",
    concattedMinifiedJsFilename: "main.min.js",
    watchPaths: {
        scss: "_src/scss/**/*.scss",
        js: "_src/js/**/*.js",
        cshtml: "Views/**/*.cshtml"
    },
    browserSyncSettings: {
        proxy: "http://localhost:12259/",
        host: "localhost"
    },
};
