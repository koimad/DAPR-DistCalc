kubectl create secret generic cert-secret-store --from-file .\secrets\

# View registered secret
kubectl get secrets


# views registered secrets key and content size
kubectl describe secrets cert-secret-store

#View Contents 
kubectl get secret cert-secret-store -o jsonpath='{.data}'