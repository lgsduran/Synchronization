#!/usr/bin/bash

directories=("var/tmp/srcFolder" "var/tmp/destFolder")

for directory in "${directories[@]}";
do
  suso echo $directory
  sudo mkdir -p $directory
done
