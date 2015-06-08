set INC_DIR1="d:/devel_open/eclipse/tools_ruby/delete_file"
set INC_DIR2="d:/devel_open/eclipse/tools_ruby/common"
ruby -I %INC_DIR1% -I %INC_DIR2% make_dist.rb
pause
