const gulp = require('gulp');

require('require-dir',{ extensions: ['.js'] })('./src/gulp/tasks');

 

const adminTasks = gulp.parallel('a:css', 'a:js')
const PortalTasks = gulp.parallel('p:css', 'p:js')

gulp.task("default",gulp.parallel(adminTasks,PortalTasks));