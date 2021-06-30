"use strict";

const { VueLoaderPlugin } = require("vue-loader");
const path = require("path");


module.exports = {
    mode: "development",
    entry: {
        "shows": "./web-src/views/Shows/Shows.js"
    },
    output: {
        path: path.resolve(__dirname, "wwwroot"),
        filename: "views/[name].js"
    },
    module: {
        rules: [
            { test: /\.vue$/, use: "vue-loader" },
            {
                test: /\.css$/, use: [
                    "vue-style-loader",
                    "css-loader"
                ]
            }
        ]
    },
    plugins: [
        new VueLoaderPlugin()
    ],
    resolve: {
        modules: [
            path.resolve("./web-src"),
            path.resolve("./node_modules")
        ]
    }
};