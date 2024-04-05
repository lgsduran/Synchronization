#!/usr/bin/bash

src_dir=/var/tmp/srcFolder
dest_dir=/var/tmp/destFolder
log_dir=/var/tmp/logs

sudo mkdir -m 777 "${src_dir}"
sudo mkdir -m 777 "${dest_dir}"
sudo mkdir -m 777 "${log_dir}"
