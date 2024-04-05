#!/usr/bin/bash

directories=("var/tmp/srcFolder" "var/tmp/destFolder")

for directory in "${directories[@]}";
do
  mkdir -m 777 $directory
done
