Write-Host "Apply deployments"
kubectl apply -f api-deployment.yaml

Write-Host "Apply services"
kubectl apply -f api-svc.yaml

Write-Host "Apply scale objects (HPA)"
kubectl apply -f api-scaleobject.yaml

Write-Host "Os recursos do Kubernetes foram aplicados."