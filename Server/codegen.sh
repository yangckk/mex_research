
#Generate grpc code
	protoc -I . --csharp_out=. --cpp_out=. --grpc_out=. --plugin=protoc-gen-grpc=/usr/local/bin/grpc_csharp_plugin --plugin=protoc-gen-grpc=/usr/local/bin/grpc_cpp_plugin *.proto

