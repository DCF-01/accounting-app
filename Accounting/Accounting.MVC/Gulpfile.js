var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var cleanCSS = require('gulp-clean-css');
var del = require('del');
const { series, parallel } = require('gulp');
var stripImportExport = require('gulp-strip-import-export');
var purgeCss = require('gulp-purgecss')

function minJs(cb) {
    return gulp.src([
        './node_modules/jquery/dist/jquery.min.js',
        './node_modules/bootstrap/dist/js/bootstrap.min.js',
        './node_modules/datatables.net/js/jquery.dataTables.min.js',
        './node_modules/select2/dist/js/select2.full.min.js',
        './node_modules/moment/min/moment.min.js',
        './node_modules/popper.js/dist/umd/popper.min.js',
        './node_modules/admin-lte/dist/js/adminlte.min.js',
        './Resources/js/panel.js'
    ])
        .pipe(stripImportExport())
        .pipe(concat('./wwwroot/static/js/main.min.js'))
        .pipe(gulp.dest('.'));
}

function minCss(cb) {
    return gulp.src([
        './node_modules/bootstrap/dist/css/bootstrap.min.css',
        './node_modules/select2/dist/css/select2.min.css',
        './node_modules/@fortawesome/fontawesome-free/css/all.min.css',
        './node_modules/admin-lte/dist/css/adminlte.min.css'
    ])
        .pipe(cleanCSS())
        .pipe(purgeCss({
                content: ['**/Views/**/*.cshtml'],
                safelist: ['select2', /^select2-\S*/, /^nav\S*/, /^main\S*/, /^sidebar\S*/, /^menu\S*/]
            })
        )
        .pipe(concat('./wwwroot/static/css/main.min.css'))
        .pipe(gulp.dest('.'));
}

function fonts(cb) {
    return gulp.src('./node_modules/@fortawesome/fontawesome-free/webfonts/*.{ttf,svg,eot,woff,woff2}')
        .pipe(gulp.dest('./wwwroot/static/webfonts/'));
}

async function clean(cb) {
    await del(["./wwwroot/static/js", "./wwwroot/static/css", "./wwwroot/static/fonts"]);
    cb();
}

exports.build = series(clean, parallel(minCss, minJs, fonts));