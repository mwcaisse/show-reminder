/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');

var libs = "./wwwroot/lib/";

gulp.task('default', function () {
    // place code for your default task here
});

gulp.task("restore:bulma", function () {
    return gulp.src([
        "node_modules/bulma/css/*.*"
    ]).pipe(gulp.dest(libs + "bulma/css"));
});

gulp.task("restore:fontawesome", function () {
    return gulp.src([
        "node_modules/@fortawesome/fontawesome-free-regular/index.js"
    ]).pipe(gulp.dest(libs + "/fontawesome/js/"));
});

gulp.task('restore:jquery', function () {
    return gulp.src([
        'node_modules/jquery/dist/*.js'
    ]).pipe(gulp.dest(libs + 'jquery/js'));
});

gulp.task('restore:lodash', function () {
    return gulp.src([
        'node_modules/lodash/lodash.js',
        'node_modules/lodash/lodash.min.js'
    ]).pipe(gulp.dest(libs + 'lodash/js'));
});

gulp.task('restore:moment', function () {
    return gulp.src([
        'node_modules/moment/moment.js',
        'node_modules/moment/min/moment.min.js'
    ]).pipe(gulp.dest(libs + 'moment/js'));
});

gulp.task('restore:q', function () {
    return gulp.src([
        'node_modules/q/q.js'
    ]).pipe(gulp.dest(libs + 'q/js'));
});

gulp.task('restore:vue', function () {
    return gulp.src([
        'node_modules/vue/dist/vue.js',
        'node_modules/vue/dist/vue.min.js'
    ]).pipe(gulp.dest(libs + 'vue/js'));
});


gulp.task('restore', gulp.series(
    'restore:bulma',
    'restore:fontawesome',
    'restore:jquery',
    'restore:lodash',
    'restore:moment',
    'restore:q',
    'restore:vue'
));