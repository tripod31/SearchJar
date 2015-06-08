set INC_DIR1="C:/devel_open/netbeans/delete_file/lib"
set INC_DIR2="C:/devel_open/netbeans/common"
ruby -I %INC_DIR1% -I %INC_DIR2% make_dist.rb
pause