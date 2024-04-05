#!/bin/bash

for directory in var/tmp/srcFolder var/tmp/destFolder;
do
  mkdir -m 777 -p $directory
done
