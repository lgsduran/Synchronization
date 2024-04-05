#!/usr/bin/bash

directories=("var/tmp/srcFolder" "var/tmp/destFolder")

for directory in "${directories[@]}";
do
  sudo echo $directory
  sudo mkdir -p $directory
done
