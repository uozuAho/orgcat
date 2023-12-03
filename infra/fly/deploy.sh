#!/bin/bash
set -eu

# note that using a relative path for configpath will not work (at least on windows)
configpath=$(realpath fly.toml)
fly deploy -c $configpath ../../src
