#!/bin/bash
set -eu

# HACK: fly deploy config flag is broken
# I should be able to just run fly deploy -c fly.toml ../../src
cp fly.toml ../../src
pushd ../../src
fly deploy
rm fly.toml
popd
