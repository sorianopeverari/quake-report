![QuakeReport](https://raw.githubusercontent.com/sorianopeverari/quake-report/main/docs/logo.png)

# Quake Report
 
Quake Report is a system to get Quake III Arena game statistics information from the server log file.

# Table of contents

- [Prerequisites](#Prerequisites)
- [Installation](#Installation)
- [Usage](#Usage)
- [License](#License)
- [Author](#Author)
- [Thanks](#Thanks)

# Prerequisities

You will need to install Docker.

* [Windows](https://docs.docker.com/windows/started)
* [OS X](https://docs.docker.com/mac/started/)
* [Linux](https://docs.docker.com/linux/started/)

# Installation

Running your operating system's terminal console in the main directory, type the following command:

Building Docker image:

```shell
sudo docker build . -t quake-report:1.0
```

# Usage

Running Docker Container and remove after:

```shell
sudo docker run -it --rm quake-report:1.0
```

# License

Licensed under the [MIT License](/LICENSE), Copyright (c) 2023 - Felipe Soriano Peverari

# Author

[Felipe Soriano Peverari](https://github.com/sorianopeverari)

# Thanks

Thanks to Victor Duarte, supporter of this project for the hiring process.

