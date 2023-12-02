#!/bin/bash
# HACK: fly deploy config flag is broken
cp fly.toml ../../src
pushd ../../src
fly deploy
rm fly.toml
popd
