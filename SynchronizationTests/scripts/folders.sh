#!/usr/bin/bash

directories=(var/tmp/srcFolder var/tmp/destFolder)

for directory in "${directories[@]}";
do
  sudo mkdir -p $directory
done
