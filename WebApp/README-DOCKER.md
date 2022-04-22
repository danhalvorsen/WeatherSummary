
# How to setup the image 

```
docker build -t sample:dev .
```

# How to run the docker image
```
docker run -it --rm -v ${PWD}:/app -v /app/node_modules -p 4001:4000 -e CHOKIDAR_USEPOLLING=true sample:dev
```

# References
https://mherman.org/blog/dockerizing-a-react-app/

