#!/bin/sh
echo ""
echo "Compiling NativeCode.c..."

J:/2020.3.1f1/Editor/Data/PlaybackEngines/AndroidPlayer/NDK/build/ndk-build.cmd NDK_PROJECT_PATH=. NDK_APPLICATION_MK=Application.mk $*
mv libs/armeabi-v7a/libnative.so ..

echo ""
echo "Cleaning up / removing build folders..."  #optional..
rm -rf libs
rm -rf obj

echo ""
echo "Done!"

sleep  30
'libs/armeabi/libnative.so':