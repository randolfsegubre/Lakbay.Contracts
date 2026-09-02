import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  schema: '../schema/lakbay.graphql',
  generates: {
    'generated/types.ts': {
      plugins: ['typescript'],
      config: {
        scalars: {
          DateTime: 'string',
        },
        enumsAsTypes: false,
        avoidOptionals: false,
      },
    },
  },
};

export default config;
