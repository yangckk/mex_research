
FROM debian:stretch

RUN apt-get update && apt-get install -y \
  build-essential autoconf libtool git pkg-config curl \
  automake libtool curl make g++ unzip \
  && apt-get clean

# install protobuf first, then grpc
ENV GRPC_RELEASE_TAG v1.21.x
RUN git clone -b ${GRPC_RELEASE_TAG} https://github.com/grpc/grpc /var/local/git/grpc && \
    cd /var/local/git/grpc && \
    git submodule update --init && \
    echo "--- installing protobuf ---" && \
    cd third_party/protobuf && \
    git submodule update --init && \
    ./autogen.sh && ./configure --enable-shared && \
    make -j$(nproc) && make -j$(nproc) check && make install && make clean && ldconfig && \
    echo "--- installing grpc ---" && \
    cd /var/local/git/grpc && \
    make -j$(nproc) && make install && make clean && ldconfig && \
    rm -rf /var/local/git/grpc

RUN ls /usr/local/lib

ENV LD_LIBRARY_PATH /usr/local/lib

RUN mkdir /home/mex_research

COPY Server/ /home/mex_research

WORKDIR /home/mex_research

RUN chmod +x codegen.sh

RUN ./codegen.sh

RUN make

EXPOSE 50051

CMD /home/mex_research/UnityServer