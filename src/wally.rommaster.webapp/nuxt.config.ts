import { defineNuxtConfig } from 'nuxt'

// https://v3.nuxtjs.org/api/configuration/nuxt.config
export default defineNuxtConfig({
    /*components: {
        global: true,
        dirs: ['~/components']
    },*/
    plugins: [
        // '@/plugins/fontawesome'
    ],
    css: [
        '@fortawesome/fontawesome-svg-core/styles.css',
        'assets/css/fontawesome-free-6.1.2-web/css/all.css'
    ]
})
