export async function GET(request: Request) {
    const data = {
        "items": [
            {
                "id": "82ddb194-223f-49d9-9e2d-6da5328c0933",
                "name": "d:\\"
            }
        ],
        "pageInfo": {
            "index": 0,
            "size": 1,
            "totalItems": 1
        }
    };
    
  return new Response(JSON.stringify(data),
      {
          status: 200,
          headers: {
              'content-type': 'application/json',
          },
      })
}
