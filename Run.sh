#!/bin/bash

compiler=csc
if command -v mono-csc >/dev/null 2>&1; then
  compiler=mono-csc
fi

${compiler} -debug:full *.cs

mono --debug Main.exe