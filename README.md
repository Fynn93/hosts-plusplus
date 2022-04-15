# hosts-plusplus
Making Host files better...

## Usage
Just put some of the commands in your file and the Transpiler will do everything else.

## Syntax
### Include a file
```
##include <url>
```
#### Example
```
##include https://dbl.oisd.nl
```

### Include Urls in File
```
##foreach(##include <url>)
```
#### Example
```
##foreach(##include https://v.firebog.net/hosts/lists.php?type=nocross)
```

### Set Custom Options
```
##config <config name>
```
#### Note: The only current available config variable is `replace_0s`
#### Example
```
##config replace_0s
```

## Full File Example
```
##include https://raw.githubusercontent.com/x0uid/SpotifyAdBlock/master/hosts
##include https://dbl.oisd.nl
##foreach(##include https://v.firebog.net/hosts/lists.php?type=nocross)
##include https://www.github.developerdan.com/hosts/lists/ads-and-tracking-extended.txt
##config replace_0s
yespleasepornxxx.com
dirkcaberxxx.com
manselfie.com
pr0nhub.com
goatse.me
cp.cloudflare.comnn
ipv4only.arpa
one.one.one.one
```
