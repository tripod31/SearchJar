# encoding: utf-8

require "FileUtils"
require "common"

APP_NAME="searchjar"
COPY_FILES=
    [
    "bin/release/searchjar.exe",
    "bin/release/common.dll",
    "bin/release/ICSharpCode.SharpZipLib.dll",
    "readme.txt",
    "COPYING",
    ]
DIST_DIR="d:/devel_open/dotnet/serchjar"
TEMP_DIR="d:/work"

SRC_DIR=File.expand_path(File.dirname($0))
WORK_DIR=TEMP_DIR+"/"+APP_NAME
ZIP_FILE=APP_NAME+".zip"

FileUtils.rm_rf WORK_DIR
FileUtils.mkdir WORK_DIR
COPY_FILES.each { |f|
    FileUtils.cp f,WORK_DIR
}

FileUtils.cd TEMP_DIR
FileUtils.rm ZIP_FILE,{:force=>true}

#zip.exeでハングするためziprubyを使用
#puts `#{ZIP} -r #{ZIP_FILE} #{APP_NAME}/*`
ZIPDir(ZIP_FILE,APP_NAME)
FileUtils.mv ZIP_FILE, SRC_DIR

puts "#{ZIP_FILE}を作成しました。なにかキーを押してください"
#gets
