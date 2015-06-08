require "FileUtils"

require "common"
require "delete_file"

APP_NAME="searchjar"
COPY_FILES=
    [
    "bin/release/searchjar.exe",
    "bin/release/common.dll",
    "bin/release/ICSharpCode.SharpZipLib.dll",
    "readme.txt",
    "C:/devel_open/dotnet/Common/src_common.zip",
    "COPYING",
    ]
DELETE_SRC_PAT=['\.exe$','\.dll$','\.pdb$','\.suo','obj','make_dist','Release','Debug','ftp\.bat']
DIST_DIR="c:/data_priv/hp/myhp/files"

TEMP_DIR="c:/temp"
WORK_DIR=TEMP_DIR+"/"+APP_NAME
ZIP_FILE=APP_NAME+".zip"

#srcファイルコピー
FileUtils.rm_r WORK_DIR,{:force=>true}
FileUtils.mkdir WORK_DIR
FileUtils.cp_r "../#{APP_NAME}", "#{WORK_DIR}/src"
df = DeleteFile.new
df.delete_files("#{WORK_DIR}/src",DELETE_SRC_PAT , true)

COPY_FILES.each { |f|
    FileUtils.cp f,WORK_DIR
}

FileUtils.cd WORK_DIR
ZIPDir("src.zip","src")
FileUtils.rm_r "src"

FileUtils.cd TEMP_DIR
FileUtils.rm ZIP_FILE,{:force=>true}



#zip.exeでハングするためziprubyを使用
#puts `#{ZIP} -r #{ZIP_FILE} #{APP_NAME}/*`
ZIPDir(ZIP_FILE,APP_NAME)

#FileUtils.mv ZIP_FILE, DIST_DIR

puts "#{TEMP_DIR}/#{ZIP_FILE}を作成しました。なにかキーを押してください"
#gets
