#!/bin/bash
touch output.log
if ! [ -x "$(command -v npm)" ]; then
  echo 'Error: npm is not installed.'
  exit 1
fi

# args=("$@")
# echo "Path->"  ${args[0]} > output.log

cd ../Build && npx xrpk init && npx xrpk build . package.wbn && mv package.wbn .. > output.log

echo "Packaged!"
