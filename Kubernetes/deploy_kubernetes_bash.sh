#!/bin/bash

export PATH=$PATH:/c/Windows/system32

echo "Apply deployments"
"$kubectl" apply -f api-deployment.yaml

echo "Apply services"
"$kubectl" apply -f api-svc.yaml

echo "Apply scale objects (HPA)"
"$kubectl" apply -f api-scaleobject.yaml

echo "Os recursos do Kubernetes foram aplicados."