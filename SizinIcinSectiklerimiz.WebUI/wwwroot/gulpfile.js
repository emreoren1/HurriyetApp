const gulp = require('gulp');
const sass = require('gulp-sass');
const del = require('del');
var watch = require('gulp-watch');

gulp.task('styles', () => {
    return gulp.src('css/**/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('css/'));
});

gulp.task('stream', function () {
    // Endless stream mode
    return watch('css/**/*.css', { ignoreInitial: false })
        .pipe(gulp.dest('build'));
});
 
gulp.task('callback', function () {
    // Callback mode, useful if any plugin in the pipeline depends on the `end`/`flush` event
    return watch('css/**/*.css', function () {
        gulp.src('css/**/*.css')
            .pipe(gulp.dest('build'));
    });
});

gulp.task('clean', () => {
    return del([
        'css/sizinIcinSectiklerimiz.css',
    ]);
});

gulp.task('default', gulp.series(['clean', 'styles']));